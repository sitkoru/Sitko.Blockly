import './index.css';

window.Blockly = {
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
