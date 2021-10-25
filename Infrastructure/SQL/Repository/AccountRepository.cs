using Core.Entities.Police;
using Core.Interfaces.RepositoryInterfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SQL.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DatabaseContext _context;
        private readonly IConfiguration _configuration;
        public AccountRepository(DatabaseContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<Person> GetPersonById(int Id)
        {
            var user = await _context.persons.Include(p => p.Role).Include(p => p.tokens).FirstOrDefaultAsync(p => p.Id == Id);
            return user;
        }

        private async Task<Person> GetPersonByName(string Name)
        {
            var user = await _context.persons.Include(p => p.Role).Include(p => p.tokens).FirstOrDefaultAsync(p => p.Name == Name);
            return user;
        }
        private async Task<Person> GetPersonByInfo(string Name, int RoleId, string HashedPassword)
        {
            var person = await _context.persons.Include(p => p.Role).FirstOrDefaultAsync(p => p.Name == Name && p.Role.Id == RoleId && p.PasswordHash == HashedPassword);
            return person;
        }

        private async Task<Person> ValidatePerson(LoginDTO user)
        {
            var HashPass = Getsha256Hash(user.Password);
            var Person = await _context.persons.Include(p => p.Role).FirstOrDefaultAsync(x => x.Name == user.Name && x.PasswordHash == HashPass);
            if (Person != null)
            {
                return Person;
            }
            return null;
        }

        public async Task<TokenResponseDTO> Register(RegisterInfosDTO dto)
        {
            var memberexist = await GetPersonByName(dto.Name) == null;
            if (memberexist)
            {
                var Person = await AddPerson(dto);
                var PersonGenerated = await GetPersonByInfo(dto.Name, dto.RoleId, Getsha256Hash(dto.Password));
                var token = await GenerateToken(PersonGenerated.Id);
                var personTokens = await AddToken(PersonGenerated.Id, token);
                if (personTokens > 0)
                {
                    return token;
                }
            }

            return null;
        }

        public async Task<TokenResponseDTO> LoginwithUserPass(LoginDTO Person)
        {
            var person = await ValidatePerson(Person);
            if (person != null)
            {
                await DeleteToken(person.Id);
                var token = await GenerateToken(person.Id);
                var personTokens = await AddToken(person.Id, token);

                if (personTokens > 0)
                {
                    return token;
                }
                return null;
            }
            return null;
        }

        public async Task<TokenResponseDTO> LoginwithRefreshToken(string RefreshToken)
        {
            var PersonToken = await FindRefreshToken(RefreshToken);
            if (PersonToken != null)
            {
                await DeleteToken(PersonToken.User.Id);
                var token = await GenerateToken(PersonToken.User.Id);
                var personTokens = await AddToken(PersonToken.User.Id, token);

                if (personTokens > 0)
                {
                    return token;
                }
                return null;
            }
            return null;
        }

        private async Task<Person> AddPerson(RegisterInfosDTO dto)
        {
            var Person = new Person()
            {
                Name = dto.Name,
                PasswordHash = Getsha256Hash(dto.Password),
                Role = await _context.roles.FindAsync(dto.RoleId),
            };
            await _context.persons.AddAsync(Person);
            var result = await _context.SaveChangesAsync();
            return Person;
        }

        private async Task<int> AddToken(int PersonId, TokenResponseDTO Token)
        {
            var token = new Token()
            {
                PersonId = PersonId,
                RefreshToken = Getsha256Hash(Token.RefreshToken),
                RefreshTokenExp = DateTime.Now.AddMinutes(int.Parse(_configuration["JWtConfig:RefreshTokenExpireshours"])),
                TokenExp = DateTime.Now.AddMinutes(int.Parse(_configuration["JWtConfig:expires"])),
                TokenHash = Getsha256Hash(Token.Token),
            };
            await _context.tokens.AddAsync(token);
            var result = await _context.SaveChangesAsync();
            return result;
        }

        private async Task<Token> FindRefreshToken(string RefreshToken)
        {
            string RefreshTokenHash = Getsha256Hash(RefreshToken);
            var PersonToken = await _context.tokens.Include(p => p.User)
                .SingleOrDefaultAsync(p => p.RefreshToken == RefreshTokenHash);
            return PersonToken;
        }

        private async Task DeleteToken(int PersonId)
        {
            var Person = await GetPersonById(PersonId);
            if (Person != null)
            {
                _context.tokens.RemoveRange(Person.tokens);
                await _context.SaveChangesAsync();
            }
        }


        private async Task<TokenResponseDTO> GenerateToken(int PersonId)
        {
            var Person = await GetPersonById(PersonId);
            var PersonRole = Person.Role;

            var claims = new List<Claim>
            {
                new Claim("Id", Person.Id.ToString() ?? throw new InvalidOperationException()),
                new Claim(ClaimTypes.Name, Person.Name ?? throw new InvalidOperationException()),
                new Claim(ClaimTypes.Role, Person.Role.RoleName ?? throw new InvalidOperationException()),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddHours(1)).ToUnixTimeSeconds().ToString()),
            };

            string key = _configuration["JWtConfig:Key"];
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenexp = DateTime.Now.AddMinutes(int.Parse(_configuration["JWtConfig:expires"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWtConfig:issuer"],
                audience: _configuration["JWtConfig:audience"],
                expires: tokenexp,
                notBefore: DateTime.Now,
                claims: claims,
                signingCredentials: credentials
                );
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new TokenResponseDTO
            {
                Token = jwtToken,
                RefreshToken = RefreshTokenGenerator()
            };
        }

        private string RefreshTokenGenerator()
        {
            var random = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(random);
            var Output = Convert.ToBase64String(random);
            return Output;
        }
        private string Getsha256Hash(string value)
        {
            var algoritm = new SHA256CryptoServiceProvider();
            var byteValue = Encoding.UTF8.GetBytes(value);
            var byteHash = algoritm.ComputeHash(byteValue);
            return Convert.ToBase64String(byteHash);
        }

        public async Task<bool> CheckExistToken(string Token)
        {
            string tokenHash = Getsha256Hash(Token);
            var userToken = await _context.tokens.Where(p => p.TokenHash == tokenHash).FirstOrDefaultAsync();
            return userToken == null ? false : true;
        }

    }
}

