using Microsoft.Extensions.Configuration;
using Promocoes.Application.Input.Commands.BusinessContext;
using Promocoes.Application.Input.Services.Jwt;

namespace Promocoes.Tests.Tests
{
    
    [TestClass]
    public class JwtTests
    {
       
        [TestMethod]
        public void Criar_Token_Jwt()
        {

            var model = new AuthenticationCommand();
            model.Email = "ramonramos.silva19@gmail.com";
            model.Password = "123456";
            var jwToken = new CreateToken(model);
            Assert.IsNotNull(jwToken.Token);
        }

        [TestMethod]
        public void Verificar_Jwt_valido()
        {
            var model = new AuthenticationCommand();
            model.Email = "ramonramos.silva19@gmail.com";
            model.Password = "123456";
            var jwToken = new CreateToken(model);
            var validateToken = ValidateToken.VerifyToken(jwToken.Token);

            Assert.AreEqual(true, validateToken);
        }
    } 
}