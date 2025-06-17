using PocheteAPI.DTO;
using System.Net.Http.Json;

namespace PocheteAPI.Services {
    public class AdminService {
        private readonly HttpClient _http;

        public AdminService(HttpClient http) {
            _http = http;
        }
        
        public async Task<List<AdminDTO>> ListarAsync() {
            return await _http.GetFromJsonAsync<List<AdminDTO>>("api/Admin");
        }

        public async Task<AdminDTO?> BuscarPorIdAsync(int id) {
            return await _http.GetFromJsonAsync<AdminDTO>($"api/Admin/{id}");
        }

        public async Task<(bool sucesso, string mensagem)> CadastrarAsync(AdminDTO admin) {
            var response = await _http.PostAsJsonAsync("api/Admin", admin);
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode) {
                return (true, "Admin cadastrado com sucesso!");
            }
            else {
                return (false, $"Erro ao cadastrar o admin: {content}");
            }
        }

        public async Task<bool> AtualizarAsync(int id, AdminDTO admin) {
            var response = await _http.PutAsJsonAsync($"api/Admin/{id}", admin);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ExcluirAsync(int id) {
            var response = await _http.DeleteAsync($"api/Admin/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<(bool sucesso, AdminDTO? admin, string mensagem)> LoginAsync(string nome, string senha) {
            var login = new LoginRequestDTO {
                Nome = nome,
                Senha = senha
            };

            var response = await _http.PostAsJsonAsync("api/Admin/login", login);
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode) {
                var admin = await response.Content.ReadFromJsonAsync<AdminDTO>();
                return (true, admin, "Login realizado com sucesso.");
            }
            else {
                return (false, null, content); // mensagem de erro
            }
        }

    }
}
