using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    class block
    {
        int Index;
        string Timestamp;
        long Proof;
        string CurrentHash;
        transaction CurrentTransaction;
        string PreviousHash;
        block(int i,string times,string ch,transaction t,string prehash)
        {
            this.Index = i;
            this.Timestamp = times;
        }

    }
}
