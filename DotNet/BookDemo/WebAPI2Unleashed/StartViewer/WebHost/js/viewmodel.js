﻿/// <reference path="knockout-3.2.0.debug.js" />
/// <reference path="jquery.min.js" />

(function () {
    function ViewModel() {
        self = this;
        self.contacts = ko.observableArray();
        self.contact = ko.observable();

        self.load = function () {
            $.get('http://localhost/webhost/api/contacts', function (data) {
                self.contacts(data);
            });
        };

        self.showDialog = function (data) {
            if (!data.Id) {
                data = {
                    Id: '',
                    Name: '',
                    PhoneNo: '',
                    EmailAddress: '',
                    Address: ''
                };
            }
            self.contact(data);
            $('.modal').modal('show');
        };

        self.save = function () {
            $('.modal').modal('hide');
            if (self.contact().Id) {
                $.ajax({
                    url: 'http://localhost/webhost/api/contacts/' + self.contact().Id,
                    type: 'PUT',
                    data: self.contact(),
                    success: function () { self.load();}
                });
            } else {
                $.ajax({
                    url: 'http://localhost/webhost/api/contacts',
                    type: 'POST',
                    data: self.contact(),
                    success: function () { self.load(); }
                });
            }
        };

        self.delete = function (data) {
            $.ajax({
                url: 'http://localhost/webhost/api/contacts/' + data.Id,
                type: 'DELETE',
                success: function () { self.load(); }
            });
        };

        self.load();
    }

    $(function () {
        ko.applyBindings(new ViewModel());
    });

})();