using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using funcionHash.Models;
using funcionHash.Helpers;
using Microsoft.Extensions.Options;

namespace funcionHash.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public string pruebaHash(string valor)
        {
            string valorReturn = funcionHA.cifrarDatos(valor);
            return valorReturn;
        }

        public bool comparar(string valorCifrado, string valornuevo)
        {
            if (funcionHA.cifrarDatos(valornuevo) != valorCifrado)
                return false;
            return true;
        }


        public string Encriptar(string Cadena)
        {
            return funcionHA.Base64_Encode(Cadena);
        }

        public string Desencriptar(string Cadena)
        {
            return funcionHA.Base64_Decode(Cadena);
        }


        public IActionResult Privacy()
        {
            return View();
        }
        /// <summary>
        /// aqui si uso el cifrado
        /// </summary>
        /// <param name="textoAcifrar"></param>
        /// <returns></returns>
        public string cifrarAES(string textoAcifrar)
        {
            string salt = "salt";
            string palabrPaso = "palabraso";
            string algoritmoCifrado = "MD5";
            int vueltas = 22;
            string num16car = "1234567891234567";
            int TAMClave = 128;



            var valor = AES.cifrarTextoAES(textoAcifrar, palabrPaso, salt, algoritmoCifrado, vueltas, num16car, TAMClave);

            return valor;
        }

        public string descifrarAES(string textoCifrado)
        {
            string salt = "salt";
            string palabrPaso = "palabraso";
            string algoritmoCifrado = "MD5";
            int vueltas = 22;
            string num16car = "1234567891234567";
            int TAMClave = 128;

            var valor = AES.descifrarTextoAES(textoCifrado, palabrPaso, salt, algoritmoCifrado, vueltas, num16car, TAMClave);
            return valor;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
