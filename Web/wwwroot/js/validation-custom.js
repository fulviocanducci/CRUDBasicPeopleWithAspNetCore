(function ($) {
   $.validator.setDefaults({
      ignore: []
   });
   $.validator.addMethod('data-val-required-date-time',
      function (value, element, params) {
         if (!value || value.toString().length < 10) {
            return false;
         }
         return moment(value, "DD/MM/YYYY", true).isValid();
      }, function (params, element) {
         var msgCompare = $(element).attr('data-val-required-date-time');
         if (!msgCompare) {
            msgCompare = 'Validate inválid';
         }
         return msgCompare;
      });
   $.validator.addMethod('data-val-required-phone',
      function (value, element, params) {
         if (!value) {
            return false;
         }
         let config = $(element).attr('data-val-required-phone-lengths');
         if (!config) {
            return false;
         }
         let items = config.split(',').map(Number);
         if (!items) {
            return false;
         }
         return items.includes(value.toString().replace(/[^0-9]/g, '').length);
      }, function (params, element) {
         var msgCompare = $(element).attr('data-val-required-phone');
         if (!msgCompare) {
            msgCompare = 'Validate inválid';
         }
         return msgCompare;
      });
   $.validator.addMethod('data-val-required-zipcode',
      function (value, element, params) {
         if (value == null) {
            return true;
         }
         const length = value.toString().replace(/[^0-9]/g, '').length;
         if (length == 0) {
            return true;
         }
         return (!(length < 8));
      }, function (params, element) {
         var msgCompare = $(element).attr('data-val-required-zipcode');
         if (!msgCompare) {
            msgCompare = 'ZipCode inválid';
         }
         return msgCompare;
      });
   $.validator.addMethod('data-val-required-one-checkbox',
      function (value, element, params) {
         const name = $(element).attr('data-val-one-checkbox-name');
         return ($('input[name="' + name + '"]:checked')).length >= 1;
      }, function (params, element) {
         var msgCompare = $(element).attr('data-val-required-one-checkbox');
         if (!msgCompare) {
            msgCompare = 'ZipCode inválid';
         }
         return msgCompare;
      });
   $.validator.addMethod('data-val-required-money',
      function (value, element, params) {
         try {
            var minimum = $(element).attr('data-val-required-money-minimum');
            var maximum = $(element).attr('data-val-required-money-maximum');
            let newValue = value.replace(".", "");
            newValue = newValue.replace(",", ".");
            newFloatValue = parseFloat(newValue);
            if (!!minimum && newFloatValue < minimum) {
               return false;
            }
            if (!!maximum) {
               if (maximum < newFloatValue) {
                  return false;
               }
            }
            return true;
         } catch (e) {
            throw e;
         }
         return false;
      }, function (params, element) {
         var msgCompare = $(element).attr('data-val-required-money');
         if (!msgCompare) {
            msgCompare = 'Not empty value';
         }
         return msgCompare;
      });
   if ($.validator && $.validator.unobtrusive && $.validator.unobtrusive.adapters) {
      $.validator.unobtrusive.adapters.addBool('data-val-required-date-time');
      $.validator.unobtrusive.adapters.addBool('data-val-required-money');
      $.validator.unobtrusive.adapters.addBool('data-val-required-phone');
      $.validator.unobtrusive.adapters.addBool('data-val-required-zipcode');
      $.validator.unobtrusive.adapters.addBool('data-val-required-one-checkbox');
   }
})(jQuery);
