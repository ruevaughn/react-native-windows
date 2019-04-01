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
    tabIndex: PropTypes.number,
    onClick: PropTypes.func.isRequired
  }

  render() {
    const { tabIndex } = this.props
    return (
      <View style={styles.button} tabIndex={tabIndex}>
        <Button
          onPress={this.props.onClick}
          title={this.props.caption}
        />
      </View>
    )
  }
}