using AutoMapper;
using Trab_Final.BaseDados.Models;
using Trab_Final.Services.DTOs;

namespace Trab_Final.Mapping
{
    public class DomainToDTOMapping :  Profile
    {
        public DomainToDTOMapping()
        {
            CreateMap<Autor, AutorDTO>().ReverseMap();
            CreateMap<Emprestimo, EmprestimoDTO>().ReverseMap();
            CreateMap<Pessoa, PessoaDTO>().ReverseMap();
            CreateMap<Editora, EditoraDTO>().ReverseMap();
            CreateMap<Livro, LivroDTO>().ReverseMap();
            CreateMap<EmprestimoLivro, EmprestimoLivroDTO>().ReverseMap();

            CreateMap<AtualizarAutorDTO, Autor>().ReverseMap();
            CreateMap<AtualizarPessoaDTO, Pessoa>().ReverseMap();
            CreateMap<AtualizarEditoraDTO, Editora>().ReverseMap();
            CreateMap<AtualizarEmprestimoDTO, Emprestimo>().ReverseMap();
            CreateMap<AtualizarLivroDTO, Livro>().ReverseMap();
            CreateMap<AtualizarEmprestimoLivroDTO, EmprestimoLivro>().ReverseMap();


            CreateMap<CriarAutorDTO, Autor>().ReverseMap();
            CreateMap<CriarPessoaDTO, Pessoa>().ReverseMap();
            CreateMap<CriarEditoraDTO, Editora>().ReverseMap();
            CreateMap<CriarEmprestimoDTO, Emprestimo>().ReverseMap();
            CreateMap<CriarLivroDTO, Livro>().ReverseMap();
            CreateMap<CriarEmprestimoLivroDTO, EmprestimoLivro>().ReverseMap();
        }
    }
}
