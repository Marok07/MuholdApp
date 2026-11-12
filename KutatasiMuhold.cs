using System;
using System.Collections.Generic;
using System.Text;

namespace MuholdApp
{
    internal class KutatasiMuhold : Muhold
    {
        public int MuszerekSzama { get; set; }
        public KutatasiMuhold(string a, int s, int m) : base(a, s)
        {
            this.MuszerekSzama = m;
        }
        public override int NapiKoltseg()
        {
            return base.NapiKoltseg() + MuszerekSzama*200;
        }
        public override string ToString()
        {
            return base.ToString() + $" | Műszerek: {MuszerekSzama} db";
        }
    }
}
