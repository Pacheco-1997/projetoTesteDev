using Domain.Valadators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public sealed class Usuario
    {
        public int Id { get; private set; }
        public string? Name { get; private set; }
        public string? Email { get; private set; }

        public Usuario()
        {
            
        }

        public Usuario(string name, string email) 
        {
            ValidateDomain(name, email);
        }

        [JsonConstructor]
        public Usuario(int id, string name, string email)
        {
            DomainValidation.When(id < 0, "Valor invalido para Id");
            Id = id;
            ValidateDomain(name, email);
        }

        public void Update(string name, string email)
        {
            ValidateDomain(name, email);
        }

        private void ValidateDomain(string nome, string email)
        {
            DomainValidation.When(string.IsNullOrEmpty(nome), "O nome deve ser preenchido");

            DomainValidation.When(string.IsNullOrEmpty(email), "O email deve ser preenchido");

            DomainValidation.When(nome.Length < 3, "O nome deve ter no minimo 3 catacteres");

            DomainValidation.When(email.Length > 250, "O email pode ter no maximo 250 catacteres");

            DomainValidation.When(IsValidEmail(email), "Formato inválido para email");

            Name = nome;
            Email = email;
        }

        static bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            return !Regex.IsMatch(email, pattern);
        }



    }
}
