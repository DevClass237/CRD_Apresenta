using PocheteAPI.DTO;
using System.Net.Http.Json;

namespace PocheteAPI.Services {
    public class SalaService {
        private readonly HttpClient _http;

        public SalaService(HttpClient http) {
            _http = http;
        }

        public async Task<List<SalasDTO>> ListarAsync() {
            return await _http.GetFromJsonAsync<List<SalasDTO>>("api/Salas");
        }

        public async Task<SalasDTO?> BuscarPorIdAsync(int id) {
            return await _http.GetFromJsonAsync<SalasDTO>($"api/Salas/{id}");
        }

        public async Task<(bool sucesso, string mensagem)> CadastrarAsync(SalasDTO sala) {
            var response = await _http.PostAsJsonAsync("api/Salas", sala);
            var content = await response.Content.ReadAsStringAsync();

            return response.IsSuccessStatusCode
                ? (true, "Sala cadastrada com sucesso!")
                : (false, $"Erro ao cadastrar a sala: {content}");
        }

        public async Task<bool> AtualizarAsync(int id, SalasDTO sala) {
            var response = await _http.PutAsJsonAsync($"api/Salas/{id}", sala);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ExcluirAsync(int id) {
            var response = await _http.DeleteAsync($"api/Salas/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
