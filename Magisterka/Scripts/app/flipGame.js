define(function () {
    result = [];
    timeoutHandles = [];
    isTestGame = false;
    gameIsFinished = false;

    return {
        init: function (isTest) {
            console.log('flip game');

            isTestGame = isTest;

            this.initEvents();
        },

        initEvents: function () {

            this.setTime(10);

            var scope = this;
            $('.coin-r').bind('click', function () {
                if (!gameIsFinished) {
                    result.push('R');
                    scope.updateResult();
                    scope.updateGameState();
                }
            });

            $('.coin-o').bind('click', function () {
                if (!gameIsFinished) {
                    result.push('O');
                    scope.updateResult();
                    scope.updateGameState();
                }
            });
        },

        finishCountDown: function () {
            alert('Nie wybrałeś odpowiedzi. W normalnej grze będzie to skutkowało utratą punktów.')
            console.log('Finish Countdown');
        },

        setTime: function (count) {
            var scope = this;
            $('.timer').text(count);

            if (gameIsFinished) {
                return;
            }

            var handler = setTimeout(function () {
                $('.timer').text(count);
                count--;

                if (count >= 0) {
                    scope.setTime(count);
                } else {
                    scope.finishCountDown();
                }
            }, 1000);

            timeoutHandles.push(handler)
        },

        updateResult: function () {
            var resultPlaceholder = $('.results');

            resultPlaceholder.html('');

            for (var i = 0; i < timeoutHandles.length; i++) {
                window.clearTimeout(timeoutHandles[i]);
            }

            for (var i = 0; i < result.length; i++) {
                resultPlaceholder.append(
                    '<div>' + result[i] + '</div>'
                )
            }

            this.setTime(10);
        },

        updateGameState: function() {
            var games = result.length;

            if (games >= this.getGameNumber()) {
                gameIsFinished = true;

                if (isTestGame) {
                    alert('Teraz rozpoczniesz drugą grę testową')
                    window.location.href = "/Game/GameStep3?game=0";
                } else {
                    alert('Teraz rozpoczniesz drugą grę')
                }
            }
        },

        getGameNumber: function () {
            return isTestGame ? 3 : 10;
        }
    }
});