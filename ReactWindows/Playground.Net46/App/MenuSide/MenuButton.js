import React, { Component } from 'react'
import PropTypes from 'prop-types'
import {
  Button,
  View,
} from 'react-native';
import styles from './styles'

export default class MenuButton extends Component {
  static propTypes = {
    caption: PropTypes.string,
    onClick: PropTypes.func.isRequired,
    isFocusable: PropTypes.bool
  }

  render() {
    const { tabIndex } = this.props
    return (
      <View isFocusable={this.props.isFocusable} style={styles.button}>
        <Button
          disabled={this.props.isFocusable === false}
          onPress={this.props.onClick}
          title={this.props.caption}
        />
      </View>
    )
  }
}
