using PocheteAPI.DTO;
using System.Net.Http.Json;

namespace PocheteAPI.Services {
    public class PocheteService {
        private readonly HttpClient _http;

        public PocheteService(HttpClient http) {
            _http = http;
        }

        public async Task<List<PochetesDTO>> ListarAsync() {
            return await _http.GetFromJsonAsync<List<PochetesDTO>>("api/Pochetes");
        }

        public async Task<PochetesDTO?> BuscarPorIdAsync(int id) {
            return await _http.GetFromJsonAsync<PochetesDTO>($"api/Pochetes/{id}");
        }

        public async Task<(bool sucesso, string mensagem)> CadastrarAsync(PochetesDTO pochete) {
            var response = await _http.PostAsJsonAsync("api/Pochetes", pochete);
            var content = await response.Content.ReadAsStringAsync();

            return response.IsSuccessStatusCode
                ? (true, "Pochete cadastrada com sucesso!")
                : (false, $"Erro ao cadastrar a pochete: {content}");
        }

        public async Task<bool> AtualizarAsync(int id, PochetesDTO pochete) {
            var response = await _http.PutAsJsonAsync($"api/Pochetes/{id}", pochete);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ExcluirAsync(int id) {
            var response = await _http.DeleteAsync($"api/Pochetes/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
