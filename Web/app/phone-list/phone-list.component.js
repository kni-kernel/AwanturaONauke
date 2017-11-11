angular.
module('phoneList').
component('phoneList', {
  templateUrl: "phone-list/phone-list.template.html",

  controller: function PhoneListController($http) {
    var self = this;
    self.orderProp = "age";

    $http.get('phones/phones.json').then(function(response) {
      self.phones = response.data;
    });
  }
});