using PocheteAPI.DTO;
using System.Net.Http.Json;

namespace PocheteAPI.Services
{
    public class TurmaService
    {
        private readonly HttpClient _http;

        public TurmaService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<TurmasDTO>> ListarAsync()
        {
            return await _http.GetFromJsonAsync<List<TurmasDTO>>("api/Turma");
        }

        public async Task<TurmasDTO?> BuscarPorIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<TurmasDTO>($"api/Turma/{id}");
        }

        public async Task<(bool sucesso, string mensagem)> CadastrarAsync(TurmasDTO turma)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("api/Turmas", turma); // Verifique se a URL está correta

                var content = await response.Content.ReadAsStringAsync();

                return response.IsSuccessStatusCode
                    ? (true, "Turma cadastrada com sucesso!")
                    : (false, $"Erro ao cadastrar a turma: {content}");
            }
            catch (Exception ex)
            {
                return (false, $"Ocorreu um erro: {ex.Message}");
            }
        }


        public async Task<bool> AtualizarAsync(int id, TurmasDTO turma)
        {
            var response = await _http.PutAsJsonAsync($"api/Turma/{id}", turma);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ExcluirAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/Turma/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
