namespace Ordinacija.Features.ReportPrint
{
    public class Constants
    {
        public static string PRE_SCHOOL_APPROVAL = @"
            Pacijent: {0} , dat. rođenja {1} -
            Pregledan u Poliklinici „Velisavljev“, dana ________________
            Izlečen i oporavljen sa savetom za dalje postupanje.
            
            Ovom potvrdom se potvrđujeda je sposoban/a za boravak u kolektivu.

            Novi Sad, ________________
        ";

        public static string DOCTORS_EXEMPTION = @"
            Pacijent: {0}
            Pregledan u Poliklinici „Velisavljev“, dana ________________
            Pod DG: __________________________________________
            Izlečen i oporavljen sa savetom za dalju terapiju i postupanje
            Potvrda se izdaje radi pravdanja izostanka iz škole _______________
            U dane _____________
            I u druge svrhe se ne može koristiti.
            
            Novi Sad, ______________
        ";

        public static string ALERGY_CONFIRMATION = @"
            Ime i prezime:   {0}
            Uzrast:  __________
            Ustanova koju pohada:  ____________________________
            Alergija na namirnice:  ________________________________
            Da ne konzumira navedene namirnice u periodu od  _______  meseci
            
            
            Novi Sad, ___________________
            {1}
        ";
    }
}
