using ClimaTempo.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimaTempo.Test.Application
{
    public class SenhaHelperTest
    {
        [Fact]
        public void Testa_MesmaSenhaHashesDevemSerDiferentes()
        {
            var senha = "Um@senha!2";

            var hash1 = SenhaHelper.GerarHash(senha);
            var hash2 = SenhaHelper.GerarHash(senha);

            Assert.NotEqual(hash1, hash2);
            Assert.True(SenhaHelper.VerificarSenha(senha, hash1));
            Assert.True(SenhaHelper.VerificarSenha(senha, hash2));
        }
        [Fact]
        public void Testa_GeracaoDoHash()
        {
            var senha = "Um@senha!2";

            var hash = SenhaHelper.GerarHash(senha);

            Assert.False(string.IsNullOrWhiteSpace(hash));
            Assert.NotEqual(senha, hash);
        }

        [Fact]
        public void VerificaSenha_SenhaCorreta()
        {
            var senha = "Senha123!";
            var hash = SenhaHelper.GerarHash(senha);

            var resultado = SenhaHelper.VerificarSenha(senha, hash);

            Assert.True(resultado);
        }

        [Fact]
        public void VerificaSenha_SenhaIncorreta()
        {
            var senhaCorreta = "Senha123!";
            var senhaErrada = "Senhha123!";

            var hash = SenhaHelper.GerarHash(senhaCorreta);

            var resultado = SenhaHelper.VerificarSenha(senhaErrada, hash);

            Assert.False(resultado);
        }

        [Fact]
        public void VerificaSenha_HashInvalido()
        {
            var senha = "Senha123!";
            var hashInvalido = "hash-invalido";

            Assert.Throws<System.FormatException>(() =>
            {
                SenhaHelper.VerificarSenha(senha, hashInvalido);
            });
        }
    }
}
