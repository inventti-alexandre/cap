﻿using Cap.Domain.Abstract;
using Cap.Domain.Models.Email;
using Cap.Domain.Respository;
using System;
using System.Linq;

namespace Cap.Domain.Service.Email
{
    public class EmailConfigService : IBaseService<EmailConfig>
    {
        private IBaseRepository<EmailConfig> repository;

        public EmailConfigService()
        {
            repository = new EFRepository<EmailConfig>();
        }

        public EmailConfig Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                EmailConfig item = repository.Find(id);

                if (item != null)
                {
                    item.Ativo = false;
                    return repository.Alterar(item);
                }

                return item;
            }
        }

        public EmailConfig Find(int id)
        {
            return repository.Find(id);
        }

        public int Gravar(EmailConfig item)
        {
            item.Sender = item.Sender.ToLower().Trim();
            item.SenderSmtp = item.SenderSmtp.ToLower().Trim();
            item.AlteradoEm = DateTime.Now;

            if (repository.Listar().Where(x => x.IdEmpresa == item.IdEmpresa && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Já existe configuração de envio de emails cadastrada para esta empresa");
            }

            if (item.Id == 0)
            {
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public IQueryable<EmailConfig> Listar()
        {
            return repository.Listar();
        }
    }
}
