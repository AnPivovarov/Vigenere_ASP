using System;
using Xunit;

namespace XUnit_Vigenere_ASP
{
    public class DecryptTest
    {
        [Fact]
        public void DecryptTest1() //базовая проверка расшифровки (эталон взят с онлайн калькулятора шифра Виженера)
        {
            string expected = "ёдгю пдхтыаян";
            string actual = Vigenere_ASP.Model.Vigenere.VigenereDecrypt("Шифр Виженера", "тест");
            Assert.Equal(expected, actual);
        }
    }
}
