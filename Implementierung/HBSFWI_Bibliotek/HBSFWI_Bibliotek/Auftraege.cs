//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HBSFWI_Bibliotek
{
    using System;
    using System.Collections.Generic;
    
    public partial class Auftraege
    {
        public int Auftrag_id { get; set; }
        public string Auftrag_status { get; set; }
        public System.DateTime Auftrag_datum { get; set; }
        public Nullable<int> Benutzer_id { get; set; }
        public Nullable<int> ISBN { get; set; }
        public Nullable<int> FachNr { get; set; }
    
        public virtual Benutzer Benutzer { get; set; }
        public virtual Buecher Buecher { get; set; }
    }
}
