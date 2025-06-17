using PocheteAPI.DTO;
using System.Net.Http.Json;

namespace PocheteAPI.Services {
    public class UsuarioService {
        private readonly HttpClient _http;

        public UsuarioService(HttpClient http) {
            _http = http;
        }

        public async Task<List<UsuarioDTO>> ListarAsync() {
            return await _http.GetFromJsonAsync<List<UsuarioDTO>>("api/Usuario");
        }

        public async Task<UsuarioDTO?> BuscarPorIdAsync(int id) {
            return await _http.GetFromJsonAsync<UsuarioDTO>($"api/Usuario/{id}");
        }

        public async Task<(bool sucesso, string mensagem)> CadastrarAsync(UsuarioDTO usuario) {
            var response = await _http.PostAsJsonAsync("api/Usuario", usuario);
            var content = await response.Content.ReadAsStringAsync();

            return response.IsSuccessStatusCode
                ? (true, "Usuário cadastrado com sucesso!")
                : (false, $"Erro ao cadastrar o usuário: {content}");
        }

        public async Task<bool> AtualizarAsync(int id, UsuarioDTO usuario) {
            var response = await _http.PutAsJsonAsync($"api/Usuario/{id}", usuario);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ExcluirAsync(int id) {
            var response = await _http.DeleteAsync($"api/Usuario/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<(bool sucesso, UsuarioDTO? usuario, string mensagem)> LoginAsync(string nome, string senha) {
            var login = new LoginRequestDTO {
                Nome = nome,
                Senha = senha
            };

            var response = await _http.PostAsJsonAsync("api/Usuario/login", login);
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode) {
                var usuario = await response.Content.ReadFromJsonAsync<UsuarioDTO>();
                return (true, usuario, "Login realizado com sucesso.");
            }
            else {
                return (false, null, content); // mensagem de erro
            }
        }
    }
}
