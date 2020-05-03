using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public class ProAgilRepository : IProAgilRepository
    {

        private readonly ProAgilContext _context;
        public ProAgilRepository(ProAgilContext context)
        {
            _context = context;

            // Forma Geral de impedir o Bloqueio (equivalente nolock) no banco.
            // Entretando esse comando delisga a "Rastreabilidade" do banco.
            // Outra forma é usando "AsNoTracking" em cada Web Metodo.
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        //GERAIS
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
           return (await _context.SaveChangesAsync()) > 0;
        }

        //EVENTOS        
        public async Task<Evento[]> GetAllEventoAsync(bool includePalestrantes = false)
        {
                // Pode causar um Traking (Bloqueio) no banco Pq ele consulta a base e 
                // verifica se tem registro, ai sim ele atualiza ou Deleta
                // Deve-se entao nao query usar o AsNoTraking

            IQueryable<Evento> query = _context.Eventos
            .Include(c => c.Lotes)
            .Include(c => c.RedesSociais);

            if(includePalestrantes)
            {
                query = query
                .Include(pe => pe.PalestranteEventos)
                .ThenInclude(p => p.Palestrante);
            }

            query = query
            .AsNoTracking() // Usado para nao Travar (Bloquear o banco)
            .OrderBy(c =>c.Id); //.OrderByDescending(c =>c.DataEvento);

            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventoAsyncByTema(string tema, bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos
            .Include(c => c.Lotes)
            .Include(c => c.RedesSociais);

            if(includePalestrantes)
            {
                query = query
                .Include(pe => pe.PalestranteEventos)
                .ThenInclude(p => p.Palestrante);
            }

            query = query
            .AsNoTracking() // Usado para nao Travar (Bloquear o banco)
            .OrderByDescending(c =>c.DataEvento)
            .Where(c => c.Tema.Contains(tema));

            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetEventoAsyncById(int EventoId, bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos
            .Include(c => c.Lotes)
            .Include(c => c.RedesSociais);

            if(includePalestrantes)
            {
                query = query
                .Include(pe => pe.PalestranteEventos)
                .ThenInclude(p => p.Palestrante);
            }

            query = query
            .AsNoTracking() // Usado para nao Travar (Bloquear o banco)
            .OrderBy(c => c.Id) //.OrderByDescending(c => c.DataEvento)
            .Where(c => c.Id == EventoId);

            return await query.FirstOrDefaultAsync();
        }


        public async Task<Palestrante> GetPalestranteAsync(int PalestranteId, bool IncludeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
            .Include(c => c.RedesSociais);

            if(IncludeEventos)
            {
                query = query
                .Include(pe => pe.PalestranteEventos)
                .ThenInclude(e => e.Evento);
            }

            query = query
            .AsNoTracking() // Usado para nao Travar (Bloquear o banco)
            .OrderBy(p => p.Nome)
            .Where(p => p.Id == PalestranteId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Palestrante[]> GetAllPaletrantesAsyncByName(string name, bool IncludeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
            .Include(c => c.RedesSociais);

            if(IncludeEventos)
            {
                query = query
                .AsNoTracking() // Usado para nao Travar (Bloquear o banco)
                .Include(pe => pe.PalestranteEventos)
                .ThenInclude(e => e.Evento);
            }

            query = query.Where(p => p.Nome.ToLower().Contains(name.ToLower()));

            return await query.ToArrayAsync();
        }




    }
}