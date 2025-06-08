using System.Security.Cryptography;

namespace ClimaTempo.API.Helpers
{
    public static class SenhaHelper
    {
        private const int TamanhoSalt = 16;
        private const int TamanhoHash = 32;
        private const int Iteracoes = 100_000;

        public static string GerarHash(string senha)
        {
            using var rng = RandomNumberGenerator.Create();
            byte[] salt = new byte[TamanhoSalt];
            rng.GetBytes(salt);

            using var pbkdf2 = new Rfc2898DeriveBytes(senha, salt, Iteracoes, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(TamanhoHash);

            byte[] hashCompleto = new byte[TamanhoSalt + TamanhoHash];
            Buffer.BlockCopy(salt, 0, hashCompleto, 0, TamanhoSalt);
            Buffer.BlockCopy(hash, 0, hashCompleto, TamanhoSalt, TamanhoHash);

            return Convert.ToBase64String(hashCompleto);
        }

        public static bool VerificarSenha(string senha, string hashArmazenado)
        {
            byte[] hashBytes = Convert.FromBase64String(hashArmazenado);

            byte[] salt = new byte[TamanhoSalt];
            Buffer.BlockCopy(hashBytes, 0, salt, 0, TamanhoSalt);

            using var pbkdf2 = new Rfc2898DeriveBytes(senha, salt, Iteracoes, HashAlgorithmName.SHA256);
            byte[] hashSenha = pbkdf2.GetBytes(TamanhoHash);

            for (int i = 0; i < TamanhoHash; i++)
            {
                if (hashBytes[i + TamanhoSalt] != hashSenha[i])
                    return false;
            }

            return true;
        }
    }
}
