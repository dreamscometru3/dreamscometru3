define(["/Scripts/app/flipGame.js", "/Scripts/app/logicGame.js"], function (flipGame, logicGame) {

    return {
        init: function (isTest, game) {
            var scope = this;
            if (isTest) {
                console.log('game Manager Test');
                var randomGame = Math.round(Math.random());
                this.initGame(randomGame, isTest);
            } else {
                $('.startGame').bind('click', function () {
                    $('.content').hide();
                    scope.initGame(game, isTest);
                });
            }
            $("#logicGame").hide();
        },

        initGame: function (game, isTest) {
            if (game == 0) {
                flipGame.init(isTest);
                $('.flip').show();
                return;
            }

            $('.logic').show();
            logicGame.init(isTest);
        }
    }
});