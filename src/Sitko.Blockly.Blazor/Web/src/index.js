import './index.css';

window.Blockly = {
    scroll: function (element) {
        function inOutQuad(n){
            n *= 2;
            if (n < 1) return 0.5 * n * n;
            return - 0.5 * (--n * (n - 2) - 1);
        }
        function doScrolling(elementY, duration) {
            const startingY = window.pageYOffset;
            const diff = elementY - startingY;
            let start;

            // Bootstrap our animation - it will get called right before next frame shall be rendered.
            window.requestAnimationFrame(function step(timestamp) {
                if (!start) start = timestamp;
                // Elapsed milliseconds since start of scrolling.
                const time = timestamp - start;
                // Get percent of completion in range [0, 1].
                const percent = Math.min(time / duration, 1);
                const val = inOutQuad(percent);
                window.scrollTo(0, startingY + diff * val);

                // Proceed with animation as long as we wanted it to.
                if (time < duration) {
                    window.requestAnimationFrame(step);
                }
            })
        }

        setTimeout(function () {
            const rectangle = element.getBoundingClientRect();
            const scrollTop = window.pageYOffset || document.documentElement.scrollTop;
            doScrolling(scrollTop + rectangle.top, 200);
        }, 100)
    },
    Twitter: {
        load: function () {
            let d = document;
            let s = "script";
            let id = "twitter-wjs";
            let js, fjs = d.getElementsByTagName(s)[0],
                t = window.twttr || {};
            if (d.getElementById(id)) return t;
            js = d.createElement(s);
            js.id = id;
            js.src = "https://platform.twitter.com/widgets.js";
            fjs.parentNode.insertBefore(js, fjs);
            t._e = [];
            t.ready = function (f) {
                t._e.push(f);
            };
            return t;
        },
        render: function (tweetId, container) {
            console.log('render tweet', tweetId, container);
            window.twttr = Blockly.Twitter.load();
            window.twttr.ready(function (tw) {
                console.debug("Twitter is ready");
                container.innerHTML = '';
                tw.widgets.createTweet(
                    tweetId + '', container,
                    {
                        theme: 'dark'
                    }
                );
            });
        },
        clear: function (container) {
            container.innerHTML = '';
        }
    },
    Twitch: {
        isLoaded: false,
        onReady: [],
        load: function () {
            let d = document;
            let s = "script";
            let id = "twitch-js";
            let js, fjs = d.getElementsByTagName(s)[0];
            if (d.getElementById(id)) return;
            js = d.createElement(s);
            js.id = id;
            js.src = "https://player.twitch.tv/js/embed/v1.js";
            fjs.parentNode.insertBefore(js, fjs);
            js.addEventListener('load', function () {
                Blockly.Twitch._isLoaded = true;
                Blockly.Twitch.onReady.forEach(request => {
                    request();
                });
            });
        },
        render: function (container, video, channel, collection) {
            if (!Blockly.Twitch._isLoaded) {
                this.load();
                Blockly.Twitch.onReady.push(function () {
                    Blockly.Twitch.render(container, video, channel, collection);
                });
                return;
            }
            console.log('render twitch', container, video, channel, collection);
            container.innerHTML = '';
            const twitchParams = {
                width: 854,
                height: 480,
                layout: 'video',
                autoplay: false
            };
            if (collection) {
                twitchParams.collection = collection;
                if (video) {
                    twitchParams.video = video;
                }
            } else if (channel) {
                twitchParams.channel = channel;
            } else if (video) {
                twitchParams.video = video;
            }
            new Twitch.Player(container, twitchParams);
        },
        clear: function (container) {
            container.innerHTML = '';
        }
    }
}
