/// <reference path="../../../lib/jasmine/jasmine-core.js" />
/// <reference path="../../../lib/angular/angular.js" />
/// <reference path="../../../lib/angular-mocks/angular-mocks.js" />
/// <reference path="../../../lib/angular-busy/angular-busy.js" />
/// <reference path="../../../lib/angular-ui-router/angular-ui-router.js" />
/// <reference path="../smsmanagerapp.js" />
/// <reference path="../controllers/smsmanagersendcontroller.js" />
/// <reference path="../controllers/smsmanagerbrowsecontroller.js" />
/// <reference path="../controllers/smsmanagerstatisticscontroller.js" />
/// <reference path="../services/smsmanagerservice.js" />
describe("Sms Manager Browse Controller tests", function () {
    var SmsManagerService, http;
    var error500 = 'Server error';
    var vm;

    beforeEach(function () {
        module('smsManagerApp');
    });

    beforeEach(inject(function (smsManagerService, $httpBackend, $controller) {
        SmsManagerService = smsManagerService;
        http = $httpBackend;
        vm = $controller('smsManagerBrowseController', { 'smsManagerService': SmsManagerService });
        http.whenGET(/.*/).respond(200, '');
    }));

    describe('On initial load', function () {

        it('variables should be defined.', function () {
            expect(vm.response).toEqual(undefined);
            expect(vm.dateTimeFrom).not.toEqual('');
            expect(vm.dateTimeTo).not.toEqual('');
            expect(vm.skip).not.toEqual('');
            expect(vm.take).not.toEqual('');
            expect(vm.error).toEqual(undefined);
        });

    });

    describe('On filter button pressed', function () {
        var responseMock = { "totalCount": 13, "items": [{ "dateTime": "2016-09-10T20:45:31.7504083", "mcc": "262", "country": "Germany", "from": "The Sender", "to": "+4917421293388", "price": 0.055, "state": "Success" }, { "dateTime": "2016-09-10T20:45:39.3035673", "mcc": "262", "country": "Germany", "from": "The Sender", "to": "+4917421293388", "price": 0.055, "state": "Failed" }, { "dateTime": "2016-09-10T20:47:40.9779535", "mcc": "262", "country": "Germany", "from": "The Sender", "to": "+4917421293388", "price": 0.055, "state": "Success" }, { "dateTime": "2016-09-10T20:54:44.2489814", "mcc": "262", "country": "Germany", "from": "The Sender", "to": "+4917421293388", "price": 0.055, "state": "Failed" }, { "dateTime": "2016-09-10T21:23:37.7052829", "mcc": "262", "country": "Germany", "from": "The Sender", "to": "+4917421293388", "price": 0.055, "state": "Failed" }, { "dateTime": "2016-09-10T21:24:02.6368632", "mcc": "262", "country": "Germany", "from": "The Sender", "to": "+4917421293388", "price": 0.055, "state": "Success" }, { "dateTime": "2016-09-10T22:41:41.7772944", "mcc": "262", "country": "Germany", "from": "The Sender", "to": "+4917421293388", "price": 0.055, "state": "Failed" }, { "dateTime": "2016-09-10T23:17:42.3718607", "mcc": "262", "country": "Germany", "from": "The Sender", "to": "+4917421293388", "price": 0.055, "state": "Failed" }, { "dateTime": "2016-09-10T23:21:21.0175119", "mcc": "262", "country": "Germany", "from": "The Sender", "to": "+4917421293388", "price": 0.055, "state": "Failed" }, { "dateTime": "2016-09-10T23:24:30.5391183", "mcc": "262", "country": "Germany", "from": "The Sender", "to": "+4917421293388", "price": 0.055, "state": "Success" }, { "dateTime": "2016-09-11T00:05:46.9620605", "mcc": "232", "country": "Austria", "from": "The Sender", "to": "+4317421293388", "price": 0.053, "state": "Failed" }, { "dateTime": "2016-09-11T00:05:56.4302289", "mcc": "232", "country": "Austria", "from": "The Sender", "to": "+4317421293388", "price": 0.053, "state": "Failed" }, { "dateTime": "2016-09-11T00:05:57.5922102", "mcc": "232", "country": "Austria", "from": "The Sender", "to": "+4317421293388", "price": 0.053, "state": "Success" }] };

        afterEach(function () {
            http.verifyNoOutstandingExpectation();
            http.verifyNoOutstandingRequest();
        });

        it('service should be called and on success populate result', function () {
            vm.filter();

            http.expect('GET', '../api/sms/sent.json?dateTimeFrom=' + vm.dateTimeFrom + '&dateTimeTo=' + vm.dateTimeTo + '&skip=' + 10 + '&take=' + 10).respond(200, responseMock);
            http.flush();

            expect(vm.response).not.toEqual(undefined);
            expect(vm.response.items.length).not.toEqual(0);
            expect(vm.error).toEqual(undefined);
        });

        it('service should be called and on failure show error message', function () {
            vm.filter();

            http.expect('GET', '../api/sms/sent.json?dateTimeFrom=' + vm.dateTimeFrom + '&dateTimeTo=' + vm.dateTimeTo + '&skip=' + 10 + '&take=' + 10).respond(500, error500);
            http.flush();

            expect(vm.response).toEqual(undefined);
            expect(vm.error).toEqual(error500);
        });
    });

});