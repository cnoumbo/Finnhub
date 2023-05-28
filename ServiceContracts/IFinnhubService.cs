using System;
namespace ServiceContracts
{
	public interface IFinnhubService
	{
		/// <summary>
		/// Returns company informations such as company country, currency, ...
		/// </summary>
		/// <param name="stockSymbol"></param>
		/// <returns></returns>
		Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol);

		/// <summary>
		/// Returns stock price details such as current price, change in price, ...
		/// </summary>
		/// <param name="stockSymbol"></param>
		/// <returns></returns>
		Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol);
	}
}

