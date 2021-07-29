if (!window.Blockly) {
  window.Blockly = {};
}
if (!window.Blockly.Forms) {
  window.Blockly.Forms = {
    _oldBlockPositions: [],
    scrollAnimationDuration: 200,
    savePosition: function () {
      document.querySelectorAll('.block-form').forEach(b => {
        window.Blockly._oldBlockPositions[b.id] = b.getBoundingClientRect().top;
      });
      console.log("Save block positions: ", this._oldBlockPositions);
    },
    scroll: function (element, duration) {
      function inOutQuad(n) {
        n *= 2;
        if (n < 1) return 0.5 * n * n;
        return -0.5 * (--n * (n - 2) - 1);
      }

      if (!duration) {
        duration = this.scrollAnimationDuration;
      }

      let start;
      let diff;
      const scrollTop = window.pageYOffset || document.documentElement.scrollTop;
      const scrollLeft = window.pageXOffset || document.documentElement.scrollLeft;
      if (duration > 0) {
        document.querySelectorAll('.block-form').forEach(b => {
          const rectangleAfter = b.getBoundingClientRect();
          const oldPosition = window.Blockly._oldBlockPositions[b.id];
          console.log("Scroll to block. Old position: ", oldPosition, "New position: ", rectangleAfter.top);

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
                console.log("Scroll diff: ", diff);
                // Bootstrap our animation - it will get called right before next frame shall be rendered.
                window.requestAnimationFrame(function step(timestamp) {
                  if (!start) start = timestamp;
                  // Elapsed milliseconds since start of scrolling.
                  const time = timestamp - start;
                  // Get percent of completion in range [0, 1].
                  const percent = Math.min(time / duration, 1);
                  const val = inOutQuad(percent);
                  window.scrollTo(scrollLeft, scrollTop + diff * val);

                  // Proceed with animation as long as we wanted it to.
                  if (time < duration) {
                    window.requestAnimationFrame(step);
                  } else {
                    console.log('Scroll done');
                  }
                });
              }
            });
          });


        });
      } else {
        const rectangleAfter = element.getBoundingClientRect();
        const oldPosition = window.Blockly._oldBlockPositions[element.id];
        diff = rectangleAfter.top - oldPosition;
        console.log("Scroll diff: ", diff);
        window.scrollTo(scrollLeft, scrollTop + diff);
      }
    },
  }
}
