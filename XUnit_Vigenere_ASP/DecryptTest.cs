using System;
using Xunit;

namespace XUnit_Vigenere_ASP
{
    public class DecryptTest
    {
        [Fact]
        public void DecryptTest1() //������� �������� ����������� (������ ���� � ������ ������������ ����� ��������)
        {
            string expected = "���� ��������";
            string actual = Vigenere_ASP.Model.Vigenere.VigenereDecrypt("���� ��������", "����");
            Assert.Equal(expected, actual);
        }
    }
}
