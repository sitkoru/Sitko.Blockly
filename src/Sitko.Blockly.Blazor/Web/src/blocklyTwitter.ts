import {BlocklyBase} from "./base";

class BlocklyTwitter extends BlocklyBase {

  constructor() {
    super('BlocklyTwitter');
  }

  private load(): TwitterLike {
    let d = document;
    let id = "twitter-wjs";
    let js, fjs = d.getElementsByTagName('script')[0];
    if (d.getElementById(id)) return window.twttr;
    js = d.createElement('script');
    js.id = id;
    js.src = "https://platform.twitter.com/widgets.js";
    fjs.parentNode.insertBefore(js, fjs);

    const t: { _e: Function[]; ready: (f: Function) => void } = {
      _e: [],
      ready: function (f: Function) {
        t._e.push(f);
      }
    };
    return t;
  }

  public render(tweetId: string, container: HTMLElement) {
    this.debug('render tweet', tweetId, container);
    window.twttr = this.load();
    window.twttr.ready((tw) => {
      this.debug("Twitter is ready");
      container.innerHTML = '';
      tw.widgets.createTweet(
        tweetId + '', container,
        {
          theme: 'dark'
        }
      );
    });
  }

  public clear(container: HTMLElement) {
    container.innerHTML = '';
  }
}

if (!window.Blockly) {
  window.Blockly = {};
}
if (!window.Blockly.Twitch) {
  window.Blockly.Twitter = new BlocklyTwitter();
}
