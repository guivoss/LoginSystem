using System;
using System.Text;
using System.Security.Cryptography;

namespace Projeto.Util
{
    public class Criptografia
    {
        //metodo para receber um valor string e retorna-lo
        //criptografado em formato HASH MD5
        public string MD5Encrypt(string value)
        {
            //criar uma instacia da classe que será utuilizada
            //para gerar o hash MD5 (MD5CryptoServiceProvider)
            MD5 md5 = new MD5CryptoServiceProvider();

            //realizar a criptografia do valor
            //Obs: O metodo de encryptação recebe e retorna valores em formato byte[]
            byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(value));

            //retornando o valor encryptografado em formato de string 
            string result = string.Empty;

            foreach (var item in hash)
            {
                result += item.ToString("X2"); //X2 = hexadecimal
            }

            return result;
        }

    }
}
