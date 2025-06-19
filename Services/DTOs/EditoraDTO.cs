namespace Trab_Final.Services.DTOs
{
    public class EditoraDTO
    {
        public int IdEditora { get; set; }

        public string NomeEditora { get; set; }

        public string CnpjEditora { get; set; }

    }

    public class CriarEditoraDTO
    {
        public string NomeEditora { get; set; }
        public string CnpjEditora { get; set; }
    }

    public class AtualizarEditoraDTO
    {
        public string? NomeEditora { get; set; }
        public string? CnpjEditora { get; set; }
    }
}
