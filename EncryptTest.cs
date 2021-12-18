using System;
using Xunit;
namespace XUnit_Vigenere_ASP
{
    public class EncryptTest
    {
        [Fact]
        public void EncryptTest1() //базовая проверка шифровки (эталон взят с онлайн калькулятора шифра Виженера)
        {
            string expected = "хлрь б пюкьы дшхтц цобнрюё";
            string actual = Vigenere_ASP.Model.Vigenere.VigenereEncrypt("Карл у Клары украл кораллы", "кларнет");
            Assert.Equal(expected, actual);
        }
    }
}
