using System;
using System.Collections.Generic;
using System.Text;

namespace WpfApp1.ViewModels
{
    public class Person
    {
        private string v1;
        private string v2;
        private object dob;

        public Person(string v1, string v2, object dob)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.dob = dob;
        }
    }
}
