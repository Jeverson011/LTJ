using AutoMapper;
using Ltj.Shared.Entities;
using Ltj.Shared.Model;

namespace Ltj.Shared.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Funcionario, FuncionarioEntity>()
                .ForMember(destino => destino.Nome, opt => opt.MapFrom(origem => origem.Nome))
                .ForMember(destino => destino.CPF, opt => opt.MapFrom(origem => origem.CPF))
                .ForMember(destino => destino.PIS, opt => opt.MapFrom(origem => origem.PIS))
                .ForMember(destino => destino.DtNascimento, opt => opt.MapFrom(origem => origem.DtNascimento))
                .ForMember(destino => destino.Sexo, opt => opt.MapFrom(origem => origem.Sexo))
                .ForMember(destino => destino.Status, opt => opt.MapFrom(origem => origem.Status))
                .ForMember(destino => destino.Motivo, opt => opt.MapFrom(origem => origem.Motivo));


            #region
            CreateMap<Produto, ProdutoEntity>()
                .ForMember(destino => destino.Marca, opt => opt.MapFrom(origem => origem.Marca))
                .ForMember(destino => destino.Nome, opt => opt.MapFrom(origem => origem.Nome))
                .ForMember(destino => destino.Tipo, opt => opt.MapFrom(origem => origem.Tipo))
                .ForMember(destino => destino.PrecoCusto, opt => opt.MapFrom(origem => origem.PrecoCusto))
                .ForMember(destino => destino.PrecoVenda, opt => opt.MapFrom(origem => origem.PrecoVenda))
                .ForMember(destino => destino.Quantidade, opt => opt.MapFrom(origem => origem.Quantidade));
            #endregion
        }


    }
}
