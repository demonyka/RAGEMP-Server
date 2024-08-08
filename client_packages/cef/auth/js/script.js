document.addEventListener('mousemove', function(e) {
    const character = document.querySelector('.character');
    const { clientX: mouseX, clientY: mouseY } = e;
    
    const { innerWidth: windowWidth, innerHeight: windowHeight } = window;
    const { offsetWidth: charWidth, offsetHeight: charHeight } = character;

    const charCenterX = charWidth / 2;
    const charCenterY = charHeight / 2;
    const windowCenterX = windowWidth / 2;
    const windowCenterY = windowHeight / 2;
    
    const offsetX = (mouseX - windowCenterX) / windowCenterX * 5;
    
    character.style.transform = `translateX(${offsetX}px)`;
  });