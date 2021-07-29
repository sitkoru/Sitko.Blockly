if (!window.Blockly) {
  window.Blockly = {};
}
if (!window.Blockly.Twitter) {
  window.Blockly.Twitter = {
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
  }
}
