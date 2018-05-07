using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace courseWORK.Models
{
    public class UserState :IComparable
    {
        [DisplayName("Логин")]
        public string Email { get; set; }
        [DisplayName("Количество игр")]
        public string GameCount { get; set; }
        [DisplayName("Количество выйгрышей")]
        public string Point { get; set; }
        [DisplayName("Процент побед")]
        public string CountWin
        {
            get
            {
                if(GameCount==null || GameCount=="0")
                {
                    return "0";
                }
                return ((double)int.Parse(Point) / int.Parse(GameCount)).ToString();
            }
        }
        public UserState(string Email,string GameCount,string Point)
        {
            this.Email = Email;
            this.GameCount = GameCount;
            this.Point = Point;
        }

        public int CompareTo(object obj)
        {
            if (int.Parse(this.Point) > int.Parse(((UserState)(obj)).Point))
                return -1;
            if (int.Parse(this.Point) == int.Parse(((UserState)(obj)).Point))
                return 0;
            return 1;
        }
    }
}