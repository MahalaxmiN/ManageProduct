using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

class Program
{
	static void Main()
	{
		// Use a key of at least 32 characters for HS256
		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("test_secret_key_12345_1234567890123456"));
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken(
			issuer: "TestIssuer",
			audience: "TestAudience",
			claims: new[]
			{
				new Claim("sub", "testuser"),
				new Claim("name", "Test User")
			},
			expires: DateTime.Now.AddHours(1),
			signingCredentials: creds);

		var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
		Console.WriteLine(tokenString);
	}
}