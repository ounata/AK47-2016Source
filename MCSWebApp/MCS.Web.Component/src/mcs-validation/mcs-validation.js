(
  
function () {
  'use strict';  
  
  mcs.ng   
    
    .service('validateService', function () {
      var validateService = this;
      validateService.ruleSet = {
        phone: {
          required: true,
          message: 'phone number valid',
          patternDescription: 'phone number should be 15 bit',
          pattern: function (value) {
            if (!angular.isNumber(parseInt(value)) || value.length !=15) {
            return flase;
            }
            return true;
          }

        }
      }
    })
    .service('validateMessageService', function ($dialogs) {
      var validMessageService = this;
      validMessageService.processMessage = function (message, messageTargetElement) {
        if (messageTargetElement) {
          messageTargetElement.innerText = message.content;
        } else {
          $dialogs.error(message.title, message.content);
        }
      }

      
    });
}
)();