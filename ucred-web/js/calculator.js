class Calculator {
    constructor() {
        this.currentOperand = '0';
        this.previousOperand = '';
        this.operation = undefined;
        this.shouldResetScreen = false;
    }

    clear() {
        this.currentOperand = '0';
        this.previousOperand = '';
        this.operation = undefined;
        this.shouldResetScreen = false;
    }

    clearEntry() {
        this.currentOperand = '0';
        this.shouldResetScreen = false;
    }

    delete() {
        if (this.shouldResetScreen) return;
        
        if (this.currentOperand.length === 1 || this.currentOperand === '0') {
            this.currentOperand = '0';
        } else {
            this.currentOperand = this.currentOperand.slice(0, -1);
        }
    }

    appendNumber(number) {
        if (this.shouldResetScreen) {
            this.currentOperand = '0';
            this.shouldResetScreen = false;
        }

        if (number === '.' && this.currentOperand.includes('.')) return;
        
        if (this.currentOperand === '0' && number !== '.') {
            this.currentOperand = number;
        } else {
            this.currentOperand += number;
        }
    }

    chooseOperation(operation) {
        if (this.currentOperand === '') return;
        
        if (this.previousOperand !== '') {
            this.compute();
        }
        
        this.operation = operation;
        this.previousOperand = this.currentOperand;
        this.shouldResetScreen = true;
    }

    compute() {
        let computation;
        const prev = parseFloat(this.previousOperand);
        const current = parseFloat(this.currentOperand);
        
        if (isNaN(prev) || isNaN(current)) return;
        
        switch (this.operation) {
            case '+':
                computation = prev + current;
                break;
            case '−':
                computation = prev - current;
                break;
            case '×':
                computation = prev * current;
                break;
            case '÷':
                if (current === 0) {
                    alert('No se puede dividir por cero');
                    return;
                }
                computation = prev / current;
                break;
            default:
                return;
        }
        
        const expression = `${this.formatNumber(prev)} ${this.operation} ${this.formatNumber(current)}`;
        const result = this.roundResult(computation);
        
        // Add to history
        if (window.historyManager) {
            window.historyManager.addEntry(expression, result);
        }
        
        this.currentOperand = result.toString();
        this.operation = undefined;
        this.previousOperand = '';
        this.shouldResetScreen = true;
    }

    // Scientific functions
    sqrt() {
        const current = parseFloat(this.currentOperand);
        if (isNaN(current)) return;
        if (current < 0) {
            alert('No se puede calcular la raíz cuadrada de un número negativo');
            return;
        }
        
        const expression = `√(${this.formatNumber(current)})`;
        const result = this.roundResult(Math.sqrt(current));
        
        if (window.historyManager) {
            window.historyManager.addEntry(expression, result);
        }
        
        this.currentOperand = result.toString();
        this.shouldResetScreen = true;
    }

    power() {
        const current = parseFloat(this.currentOperand);
        if (isNaN(current)) return;
        
        const expression = `(${this.formatNumber(current)})²`;
        const result = this.roundResult(current * current);
        
        if (window.historyManager) {
            window.historyManager.addEntry(expression, result);
        }
        
        this.currentOperand = result.toString();
        this.shouldResetScreen = true;
    }

    sin() {
        const current = parseFloat(this.currentOperand);
        if (isNaN(current)) return;
        
        const expression = `sin(${this.formatNumber(current)})`;
        const result = this.roundResult(Math.sin(current * Math.PI / 180));
        
        if (window.historyManager) {
            window.historyManager.addEntry(expression, result);
        }
        
        this.currentOperand = result.toString();
        this.shouldResetScreen = true;
    }

    cos() {
        const current = parseFloat(this.currentOperand);
        if (isNaN(current)) return;
        
        const expression = `cos(${this.formatNumber(current)})`;
        const result = this.roundResult(Math.cos(current * Math.PI / 180));
        
        if (window.historyManager) {
            window.historyManager.addEntry(expression, result);
        }
        
        this.currentOperand = result.toString();
        this.shouldResetScreen = true;
    }

    tan() {
        const current = parseFloat(this.currentOperand);
        if (isNaN(current)) return;
        
        const expression = `tan(${this.formatNumber(current)})`;
        const result = this.roundResult(Math.tan(current * Math.PI / 180));
        
        if (window.historyManager) {
            window.historyManager.addEntry(expression, result);
        }
        
        this.currentOperand = result.toString();
        this.shouldResetScreen = true;
    }

    log() {
        const current = parseFloat(this.currentOperand);
        if (isNaN(current) || current <= 0) {
            alert('El logaritmo solo se puede calcular para números positivos');
            return;
        }
        
        const expression = `log(${this.formatNumber(current)})`;
        const result = this.roundResult(Math.log10(current));
        
        if (window.historyManager) {
            window.historyManager.addEntry(expression, result);
        }
        
        this.currentOperand = result.toString();
        this.shouldResetScreen = true;
    }

    ln() {
        const current = parseFloat(this.currentOperand);
        if (isNaN(current) || current <= 0) {
            alert('El logaritmo natural solo se puede calcular para números positivos');
            return;
        }
        
        const expression = `ln(${this.formatNumber(current)})`;
        const result = this.roundResult(Math.log(current));
        
        if (window.historyManager) {
            window.historyManager.addEntry(expression, result);
        }
        
        this.currentOperand = result.toString();
        this.shouldResetScreen = true;
    }

    exp() {
        const current = parseFloat(this.currentOperand);
        if (isNaN(current)) return;
        
        const expression = `e^${this.formatNumber(current)}`;
        const result = this.roundResult(Math.exp(current));
        
        if (window.historyManager) {
            window.historyManager.addEntry(expression, result);
        }
        
        this.currentOperand = result.toString();
        this.shouldResetScreen = true;
    }

    insertPi() {
        this.currentOperand = Math.PI.toString();
        this.shouldResetScreen = true;
    }

    insertE() {
        this.currentOperand = Math.E.toString();
        this.shouldResetScreen = true;
    }

    negate() {
        const current = parseFloat(this.currentOperand);
        if (isNaN(current)) return;
        this.currentOperand = (current * -1).toString();
    }

    roundResult(number) {
        return Math.round(number * 100000000) / 100000000;
    }

    formatNumber(number) {
        const stringNumber = number.toString();
        const decimalDigits = stringNumber.split('.')[1];
        let integerDisplay = parseInt(stringNumber.split('.')[0]).toLocaleString('es-ES');
        
        if (integerDisplay === 'NaN') {
            integerDisplay = '';
        }
        
        if (decimalDigits != null) {
            return `${integerDisplay}.${decimalDigits}`;
        } else {
            return integerDisplay;
        }
    }

    getDisplayText() {
        const currentDisplay = this.currentOperand === '' ? '0' : this.currentOperand;
        let previousDisplay = '';
        
        if (this.operation != null && this.previousOperand !== '') {
            previousDisplay = `${this.formatNumber(parseFloat(this.previousOperand))} ${this.operation}`;
        }
        
        return {
            current: currentDisplay,
            previous: previousDisplay
        };
    }
}
