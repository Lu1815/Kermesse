const onlyZeroOne = (e) => {
    const currentValue = parseInt(e.target.value, 10);

    if ((currentValue < 0)) {
        e.target.value = currentValue;
    }
}

const lowerOnly = (e) => {
    e.target.value = e.target.value.toLowerCase();
}