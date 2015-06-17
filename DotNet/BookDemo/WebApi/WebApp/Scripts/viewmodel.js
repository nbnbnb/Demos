/// <reference path="knockout-3.2.0.js" />

function ViewModel() {
    var self = this;
    self.contacts = ko.observableArray();
    self.contact = ko.observable();

    self.load = function () {
        // TODO: 火狐 Chrome 下跨域问题，如何解决？？？？
        $.ajax({
            url: 'http://localhost/webhost/api/contacts?timestamp' + new Date().getTime(),
            type: 'GET',
            success: function (result) {
                self.contacts(result);
            }
        });
    };

    self.showDialog = function (data) {
        if (!data.Id) {
            data = { Id: '', Name: '', PhoneNo: '', EmailAddress: '', Address: '' }
        }
        self.contact(data);
        $(".modal").modal('show');
    };

    self.save = function () {
        $('.modal').modal('hide');
        if (self.contact().Id) {
            $.ajax({
                url: 'http://localhost/webhost/api/contacts/' + self.contact().Id,
                type: 'PUT',
                data: self.contact(),
                success: function () { self.load(); }
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