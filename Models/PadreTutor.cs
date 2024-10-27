namespace BackendAPIConsumer.Models
{
    public class PadreTutor
    {
        public string Nombre { get; set; }
        public string CURP { get; set; }
        public string ScanINE { get; set; }  // URL de la imagen del INE
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string ScanComprobanteDomicilio { get; set; } // URL del comprobante de domicilio
    }
}
