function isURL(str) {
   var urlRegex = '^(?!mailto:)(?:(?:http|https|ftp)://)(?:\\S+(?::\\S*)?@)?(?:(?:(?:[1-9]\\d?|1\\d\\d|2[01]\\d|22[0-3])(?:\\.(?:1?\\d{1,2}|2[0-4]\\d|25[0-5])){2}(?:\\.(?:[0-9]\\d?|1\\d\\d|2[0-4]\\d|25[0-4]))|(?:(?:[a-z\\u00a1-\\uffff0-9]+-?)*[a-z\\u00a1-\\uffff0-9]+)(?:\\.(?:[a-z\\u00a1-\\uffff0-9]+-?)*[a-z\\u00a1-\\uffff0-9]+)*(?:\\.(?:[a-z\\u00a1-\\uffff]{2,})))|localhost)(?::\\d{2,5})?(?:(/|\\?|#)[^\\s]*)?$';
   var url = new RegExp(urlRegex, 'i');
   return url.test(str);
}

function isValidCPF(cpf) {
   if (typeof cpf !== 'string') return false
   cpf = cpf.replace(/[^\d]+/g, '')
   if (cpf.length !== 11 || !!cpf.match(/(\d)\1{10}/)) return false
   cpf = cpf.split('')
   const validator = cpf
      .filter((digit, index, array) => index >= array.length - 2 && digit)
      .map(el => +el)
   const toValidate = pop => cpf
      .filter((digit, index, array) => index < array.length - pop && digit)
      .map(el => +el)
   const rest = (count, pop) => (toValidate(pop)
      .reduce((soma, el, i) => soma + el * (count - i), 0) * 10) % 11 % 10
   return !(rest(10, 2) !== validator[0] || rest(11, 1) !== validator[1])
}

function isValidCNPJ(cnpj) {

   cnpj = cnpj.replace(/[^\d]+/g, '');

   if (cnpj == '') return false;

   if (cnpj.length != 14)
      return false;

   // Elimina CNPJs invalidos conhecidos
   if (cnpj == "00000000000000" ||
      cnpj == "11111111111111" ||
      cnpj == "22222222222222" ||
      cnpj == "33333333333333" ||
      cnpj == "44444444444444" ||
      cnpj == "55555555555555" ||
      cnpj == "66666666666666" ||
      cnpj == "77777777777777" ||
      cnpj == "88888888888888" ||
      cnpj == "99999999999999")
      return false;

   // Valida DVs
   tamanho = cnpj.length - 2
   numeros = cnpj.substring(0, tamanho);
   digitos = cnpj.substring(tamanho);
   soma = 0;
   pos = tamanho - 7;
   for (i = tamanho; i >= 1; i--) {
      soma += numeros.charAt(tamanho - i) * pos--;
      if (pos < 2)
         pos = 9;
   }
   resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
   if (resultado != digitos.charAt(0))
      return false;

   tamanho = tamanho + 1;
   numeros = cnpj.substring(0, tamanho);
   soma = 0;
   pos = tamanho - 7;
   for (i = tamanho; i >= 1; i--) {
      soma += numeros.charAt(tamanho - i) * pos--;
      if (pos < 2)
         pos = 9;
   }
   resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
   if (resultado != digitos.charAt(1))
      return false;

   return true;

}

function setClipboard(value) {
   navigator.clipboard.writeText(value);
}

$(document).ready(function () {
   moment.locale('pt-br');
});
