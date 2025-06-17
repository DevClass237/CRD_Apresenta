using PocheteAPI.DTO;
using System.Net.Http.Json;

namespace PocheteAPI.Services
{
    public class MovimentacaoService
    {
        private readonly HttpClient _http;

        public MovimentacaoService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<MovimentacaoDTO>> ListarAsync()
        {
            return await _http.GetFromJsonAsync<List<MovimentacaoDTO>>("api/movimentacoes");
        }

        public async Task<MovimentacaoDTO?> BuscarPorIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<MovimentacaoDTO>($"api/movimentacoes/{id}");
        }

        public async Task<(bool sucesso, string mensagem)> RegistrarRetiradaAsync(MovimentacaoDTO movimentacao)
        {
            var response = await _http.PostAsJsonAsync("api/movimentacoes/retirada", movimentacao);
            var content = await response.Content.ReadAsStringAsync();

            return response.IsSuccessStatusCode
                ? (true, "Retirada registrada com sucesso!")
                : (false, $"Erro ao registrar retirada: {content}");
        }

        public async Task<(bool sucesso, string mensagem)> RegistrarDevolucaoAsync(MovimentacaoDTO movimentacao)
        {
            var response = await _http.PutAsJsonAsync("api/movimentacoes/devolucao", movimentacao);
            var content = await response.Content.ReadAsStringAsync();

            return response.IsSuccessStatusCode
                ? (true, "Devolução registrada com sucesso!")
                : (false, $"Erro ao registrar devolução: {content}");
        }

        public async Task<bool> AtualizarAsync(int id, MovimentacaoDTO movimentacao)
        {
            var response = await _http.PutAsJsonAsync($"api/movimentacoes/{id}", movimentacao);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ExcluirAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/movimentacoes/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
