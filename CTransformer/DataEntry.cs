using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTransformer
{
    class DataEntry
    {
        public bool isDateValid;
        public DateTime dt;
        public float[] data;
        public DataEntry(string dataString)
        {
           
            char[] whitespace = new char[] { ' ', '\t', '\n'};
            string[] splittedDataString = dataString.Split(whitespace);
            isDateValid = DateTime.TryParse($"{splittedDataString[0]} {splittedDataString[1]}", out dt);
            data = new float[splittedDataString.Length];
            for (int i = 2; i < splittedDataString.Length; i++)
            {
                float.TryParse(splittedDataString[i], out data[i - 2]);
            }
        }

        public override string ToString()
        {
            return $"Dt.: {dt.ToString("G")} St.: {isDateValid.ToString()} {String.Join("\t", data)}";// data.ToString()}";
        }


    }
}


//for j:=0 to kol-1 do
//         begin
//          if (pan.onemr = 1)and(j = 0) then
//            begin
//               dansr:= 3.6* (data[6]+256* data[7]-133* not(pan.res1))/(pan.kemr*60);
//               if dansr< 0 then dansr:=0;
//               st:=st+' '+floattostrf(dansr, fffixed,6,2);
//end                      else
//            begin
//             val:= data[6 + 2 * j]+256* data[7 + 2 * j];
//             if val = -25536 then
//              begin
//               val:=valb[j];
//               st:=st+' '+floattostrf(val/50, fffixed,6,2);
//valb[j]:= -25536;
//              end else
//              begin
//               st:=st+' '+floattostrf(val/50, fffixed,6,2);
//valb[j]:=val;
//              end;
//            end;
//         end;
//       end