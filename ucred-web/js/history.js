class HistoryManager {
    constructor() {
        this.history = this.loadHistory();
        this.isVisible = false;
    }

    loadHistory() {
        const saved = localStorage.getItem('calculator-history');
        return saved ? JSON.parse(saved) : [];
    }

    saveHistory() {
        localStorage.setItem('calculator-history', JSON.stringify(this.history));
    }

    addEntry(expression, result) {
        const entry = {
            expression,
            result,
            timestamp: new Date().toISOString()
        };
        
        this.history.unshift(entry);
        
        // Keep only last 50 entries
        if (this.history.length > 50) {
            this.history = this.history.slice(0, 50);
        }
        
        this.saveHistory();
        this.render();
    }

    clearHistory() {
        this.history = [];
        this.saveHistory();
        this.render();
    }

    toggle() {
        this.isVisible = !this.isVisible;
        const wrapper = document.querySelector('.calculator-wrapper');
        
        if (this.isVisible) {
            wrapper.classList.add('show-history');
        } else {
            wrapper.classList.remove('show-history');
        }
    }

    render() {
        const historyList = document.getElementById('history-list');
        
        if (this.history.length === 0) {
            historyList.innerHTML = '<p class="history-empty">No hay operaciones aún</p>';
            return;
        }
        
        historyList.innerHTML = this.history
            .map(entry => `
                <div class="history-item" data-result="${entry.result}">
                    <div class="history-expression">${this.escapeHtml(entry.expression)}</div>
                    <div class="history-result">= ${this.formatResult(entry.result)}</div>
                </div>
            `)
            .join('');
        
        // Add click handlers to history items
        historyList.querySelectorAll('.history-item').forEach(item => {
            item.addEventListener('click', () => {
                const result = item.dataset.result;
                if (window.calculator) {
                    window.calculator.currentOperand = result;
                    window.calculator.shouldResetScreen = true;
                    updateDisplay();
                }
            });
        });
    }

    formatResult(result) {
        const num = parseFloat(result);
        if (isNaN(num)) return result;
        
        // Format with up to 8 decimal places, removing trailing zeros
        return num.toLocaleString('es-ES', {
            maximumFractionDigits: 8,
            useGrouping: true
        });
    }

    escapeHtml(text) {
        const div = document.createElement('div');
        div.textContent = text;
        return div.innerHTML;
    }
}
