namespace ParkingOnline.UI.Models;

public class ListaPaginada<T> : List<T>
{
    public int IndicePagina { get; private set; }

    public int TotalPaginas { get; private set; }

    public ListaPaginada(List<T> itens, int quantidade, int indicePagina, int tamanhoPagina)
    {
        IndicePagina = indicePagina;
        TotalPaginas = (int)Math.Ceiling(quantidade / (double)tamanhoPagina);
        AddRange(itens);
    }

    public bool TemPaginaAnterior => IndicePagina > 1;

    public bool TemProximaPagina => IndicePagina < TotalPaginas;

    public static ListaPaginada<T> Create(IQueryable<T> fonte, int indicePagina, int tamanhoPagina)
    {
        var count = fonte.Count();
        var items = fonte.Skip((indicePagina - 1) * tamanhoPagina).Take(tamanhoPagina).ToList();
        return new ListaPaginada<T>(items, count, indicePagina, tamanhoPagina);
    }
}