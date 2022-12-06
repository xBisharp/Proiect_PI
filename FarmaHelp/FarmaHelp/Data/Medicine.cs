namespace FarmaHelp.Data
{
    public class Medicine
    {
        public string name{get;set;}
        public int id { get; set; }
        public double pret{get;set;}
        public bool isInCart{get;set;}
        public Medicine() : this("",0,0.0f)
        {

        }

        public Medicine(string name, int id, float pret)
        {
            this.name = name;
            this.id = id;
            this.pret = pret;
        }


    }
}