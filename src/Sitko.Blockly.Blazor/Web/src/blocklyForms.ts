import {BlocklyBase} from "./base";

class BlocklyForms extends BlocklyBase {
  private _oldBlockPositions: { [id: string]: number; } = {};
  private _scrollAnimationDuration = 200;

  constructor() {
    super('BlocklyForms');
  }

  private static inOutQuad(n: number) {
    n *= 2;
    if (n < 1) return 0.5 * n * n;
    return -0.5 * (--n * (n - 2) - 1);
  }

  public savePosition() {
    document.querySelectorAll('.block-form').forEach(b => {
      this._oldBlockPositions[b.id] = b.getBoundingClientRect().top;
    });
    ``
    this.debug("Save block positions: ", this._oldBlockPositions);
  }

  public scroll(element: HTMLElement, duration?: number) {
    if (!duration) {
      duration = this._scrollAnimationDuration;
    }

    let start: DOMHighResTimeStamp;
    let diff: number;
    const scrollTop = window.pageYOffset || document.documentElement.scrollTop;
    const scrollLeft = window.pageXOffset || document.documentElement.scrollLeft;
    if (duration > 0) {
      document.querySelectorAll('.block-form').forEach(b => {
        const rectangleAfter = b.getBoundingClientRect();
        const oldPosition = this._oldBlockPositions[b.id];
        this.debug("Scroll to block. Old position: ", oldPosition, "New position: ", rectangleAfter.top);

        const deltaY = oldPosition - rectangleAfter.top;

        requestAnimationFrame(() => {
          element.style.transform = `translate(0, ${deltaY}px)`;
          element.style.transition = 'transform 0s';

          requestAnimationFrame(() => {
            // In order to get the animation to play, we'll need to wait for
            // the 'invert' animation frame to finish, so that its inverted
            // position has propagated to the DOM.
            //
            // Then, we just remove the transform, reverting it to its natural
            // state, and apply a transition so it does so smoothly.
            element.style.transform = '';
            element.style.transition = `transform ${duration}ms`;
            if (b === element) {
              diff = rectangleAfter.top - oldPosition;
              this.debug("Scroll diff: ", diff);
              // Bootstrap our animation - it will get called right before next frame shall be rendered.
              const step = (timestamp: DOMHighResTimeStamp) => {
                if (!start) start = timestamp;
                // Elapsed milliseconds since start of scrolling.
                const time = timestamp - start;
                // Get percent of completion in range [0, 1].
                const percent = Math.min(time / duration, 1);
                const val = BlocklyForms.inOutQuad(percent);
                window.scrollTo(scrollLeft, scrollTop + diff * val);

                // Proceed with animation as long as we wanted it to.
                if (time < duration) {
                  window.requestAnimationFrame(step);
                } else {
                  this.debug('Scroll done');
                }
              };
              window.requestAnimationFrame(step);
            }
          });
        });


      });
    } else {
      const rectangleAfter = element.getBoundingClientRect();
      const oldPosition = this._oldBlockPositions[element.id];
      diff = rectangleAfter.top - oldPosition;
      this.debug("Scroll diff: ", diff);
      window.scrollTo(scrollLeft, scrollTop + diff);
    }
  }
}

if (!window.Blockly) {
  window.Blockly = {};
}
if (!window.Blockly.Forms) {
  window.Blockly.Forms = new BlocklyForms();
}
