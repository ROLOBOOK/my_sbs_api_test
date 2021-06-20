using sbs_api_2.Models;
using System.Collections.Generic;
using System.Linq;
using LiteDB;
using Dadata;
using System.Threading.Tasks;



namespace sbs_api_2.Services

{

    public static class CounterpartyService
    {
        static async Task<string> get_fulname(string inn)
        {
            var token = "47921ef68f6565ae44282681faf892e3106f6cdf";
            var api = new SuggestClientAsync(token);
            var result =  await api.FindParty(inn);
            var name_firm = result.suggestions[0].value.ToString();
            return name_firm;
        }


        public static List<Counterparty> GetAll()
        {
            using (var db = new LiteDatabase(@"MyCounterparty.db"))
            {
                var counterparty = db.GetCollection<Counterparty>("Контрагенты");
                var result = counterparty.FindAll();
                List<Counterparty> allCounterparty = new List<Counterparty>();
                allCounterparty = result.ToList();
                return allCounterparty;
            }
        }
        static public string Insert(Counterparty counterpartySave)
        {
            if (
                    counterpartySave.Type.Equals(type_company.Юр_лицо) &&
                    counterpartySave.KPP.Equals(null)
            )
            {
                return "";
            }
            using (var db = new LiteDatabase(@"MyCounterparty.db"))
            {
              
                var counterparty = db.GetCollection<Counterparty>("Контрагенты");
                  if (counterparty.Exists(Query.EQ("Name",counterpartySave.Name)))
                {
                    return "";
                }

                var task = get_fulname(counterpartySave.INN);
                task.Wait();
                var full_name = task.Result;

                var newConterparty = new Counterparty { 
                    Name = counterpartySave.Name, 
                    INN = counterpartySave.INN,
                    KPP = counterpartySave.KPP,
                    Type = counterpartySave.Type,
                    FullName = full_name
                     }; 
                counterparty.Insert(newConterparty);
                string res = counterparty.FindOne(Query.All(Query.Descending)).Id.ToString();
                return res;
            }        
        }
        
    }
}
// - name, type, inn не могут быть пустыми, kpp не может быть пусто у Юр.лица