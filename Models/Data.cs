using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PinterestCheckLinked.Models
{
    public class Data
    {
        private string _email;
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
            }
        }
        private int _index;
        public int Index
        {
            get
            {
                return _index;
            }
            set
            {
                _index = value;
            }
        }
        private string _proxy;
        public string Proxy
        {
            get
            {
                return _proxy;
            }
            set
            {
                _proxy = value;
            }
        }
        private string _status;
        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
            }
        }
    }
}
