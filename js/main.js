// Initialize components
const calculator = new Calculator();
const themeManager = new ThemeManager();
const historyManager = new HistoryManager();

// Make calculator and history manager global for cross-component access
window.calculator = calculator;
window.historyManager = historyManager;

// DOM elements
const currentOperandElement = document.getElementById('current-operand');
const previousOperandElement = document.getElementById('previous-operand');
const numberButtons = document.querySelectorAll('[data-number]');
const operatorButtons = document.querySelectorAll('[data-action]');
const themeToggle = document.getElementById('theme-toggle');
const historyToggle = document.getElementById('history-toggle');
const clearHistoryBtn = document.getElementById('clear-history');

// Update display
function updateDisplay() {
    const display = calculator.getDisplayText();
    currentOperandElement.textContent = display.current;
    previousOperandElement.textContent = display.previous;
}

// Number buttons
numberButtons.forEach(button => {
    button.addEventListener('click', () => {
        const number = button.dataset.number;
        calculator.appendNumber(number);
        updateDisplay();
    });
});

// Action buttons
operatorButtons.forEach(button => {
    button.addEventListener('click', () => {
        const action = button.dataset.action;
        
        switch (action) {
            case 'clear-all':
                calculator.clear();
                break;
            case 'clear-entry':
                calculator.clearEntry();
                break;
            case 'backspace':
                calculator.delete();
                break;
            case 'decimal':
                calculator.appendNumber('.');
                break;
            case 'add':
                calculator.chooseOperation('+');
                break;
            case 'subtract':
                calculator.chooseOperation('−');
                break;
            case 'multiply':
                calculator.chooseOperation('×');
                break;
            case 'divide':
                calculator.chooseOperation('÷');
                break;
            case 'equals':
                calculator.compute();
                break;
            case 'negate':
                calculator.negate();
                break;
            case 'sqrt':
                calculator.sqrt();
                break;
            case 'power':
                calculator.power();
                break;
            case 'sin':
                calculator.sin();
                break;
            case 'cos':
                calculator.cos();
                break;
            case 'tan':
                calculator.tan();
                break;
            case 'log':
                calculator.log();
                break;
            case 'ln':
                calculator.ln();
                break;
            case 'exp':
                calculator.exp();
                break;
            case 'pi':
                calculator.insertPi();
                break;
            case 'e':
                calculator.insertE();
                break;
        }
        
        updateDisplay();
    });
});

// Theme toggle
themeToggle.addEventListener('click', () => {
    themeManager.toggle();
});

// History toggle
historyToggle.addEventListener('click', () => {
    historyManager.toggle();
});

// Clear history
clearHistoryBtn.addEventListener('click', () => {
    if (confirm('¿Estás seguro de que quieres borrar todo el historial?')) {
        historyManager.clearHistory();
    }
});

// Keyboard support
document.addEventListener('keydown', (e) => {
    // Numbers
    if (e.key >= '0' && e.key <= '9') {
        calculator.appendNumber(e.key);
        updateDisplay();
    }
    
    // Decimal
    if (e.key === '.' || e.key === ',') {
        calculator.appendNumber('.');
        updateDisplay();
    }
    
    // Operations
    if (e.key === '+') {
        calculator.chooseOperation('+');
        updateDisplay();
    }
    if (e.key === '-') {
        calculator.chooseOperation('−');
        updateDisplay();
    }
    if (e.key === '*') {
        calculator.chooseOperation('×');
        updateDisplay();
    }
    if (e.key === '/') {
        e.preventDefault();
        calculator.chooseOperation('÷');
        updateDisplay();
    }
    
    // Equals
    if (e.key === 'Enter' || e.key === '=') {
        calculator.compute();
        updateDisplay();
    }
    
    // Clear
    if (e.key === 'Escape') {
        calculator.clear();
        updateDisplay();
    }
    
    // Backspace
    if (e.key === 'Backspace') {
        e.preventDefault();
        calculator.delete();
        updateDisplay();
    }
});

// Initialize display and history
updateDisplay();
historyManager.render();
