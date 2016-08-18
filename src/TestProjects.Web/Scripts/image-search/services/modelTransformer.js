(function () {
    'use strict';

    angular
        .module('testProjectsApp')
        .factory("modelTransformer", modelTransformer);

    var transformGoogleResponse = function (jsonResult) {
        var images = [];
        angular.forEach(jsonResult.items, function (item) {
            var image = {
                url: item.link,
                title: item.link.split('/').pop().split('#')[0].split('?')[0],
                thumbnailUrl: item.image.thumbnailLink
            }
            images.push(image);
        });
        return images;
    };

    function modelTransformer() {
        return {
            transform: transformGoogleResponse
        }
    };

}());